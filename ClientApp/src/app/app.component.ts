import { Component, OnInit } from '@angular/core';
import { AppSettings } from './models/app.settings';
import { HttpClient } from '@angular/common/http';
import { AppService } from './app.service';

import * as signalR from "@aspnet/signalr";
import { Vote } from './models/vote.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'NgHive';
  private appSettings: AppSettings;
  votes: Vote[];
  leftSide: string;
  rightSide: string;

  constructor(private http: HttpClient, private appService: AppService) { }

  async ngOnInit() {
    // Loading app settings
    this.appSettings = await this.http
      .get<AppSettings>('assets/config.json')
      .toPromise();
    // Setting settings to app service
    this.appService.eventName = this.appSettings.eventName;
    this.appService.apiUrl = this.appSettings.apiUrl;
    // Setting up SignalR connection
    // tslint:disable-next-line: max-line-length
    const hubConnection = new signalR.HubConnectionBuilder().withUrl(`${this.appSettings.apiUrl}`).configureLogging(signalR.LogLevel.Information).build();
    hubConnection
      .start()
      .then(() => {
        console.log('Connection started!');
      })
      .catch(() => console.log('Error while establishing connection :('));
    // Set up action when votes are created or updated
    hubConnection.on('votesUpdated', (votes: Vote[]) => {
      votes.forEach((vote: Vote) => {
        const existingVote: Vote = this.votes.find(v => v.id === vote.id);
        if (existingVote) {
          existingVote.leftVotes = vote.leftVotes;
          existingVote.rightVotes = vote.rightVotes;
        } else {
          this.votes.push(vote);
        }
      });
    });
    // Load votes
    this.votes = await this.appService.getVotes();
  }

  async addVote(voteId: string, side: string) {
    await this.appService.addVote(voteId, side);
  }

  async createVote() {
    await this.appService.createVote(this.leftSide, this.rightSide);
    this.leftSide = this.rightSide = null;
  }
}
