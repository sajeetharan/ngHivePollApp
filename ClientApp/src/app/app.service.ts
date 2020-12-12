import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Vote } from './models/vote.model';

@Injectable()
export class AppService {
  public eventName: string;
  public apiUrl: string;


  constructor(private http: HttpClient) {}

  async getVotes(): Promise<Vote[]> {
    try {
      return await this.http
        .get<Vote[]>(`${this.apiUrl}vote/${this.eventName}`)
        .toPromise();
    } catch (e) {
      console.error('Failed to get Votes');
      throw e;
    }
  }

  async createVote(leftSide: string, rightSide: string): Promise<any> {
    try {
      const newVote: Vote = {
        eventName: this.eventName,
        leftSide,
        rightSide,
      };
      return await this.http
        .post(`${this.apiUrl}vote`, newVote)
        .toPromise();
    } catch (e) {
      console.error('Failed to create new Vote');
      throw e;
    }
  }

  async addVote(voteId: string, side: string): Promise<any> {
    try {
      return await this.http
        .put(`${this.apiUrl}vote/${this.eventName}/${voteId}/${side}`, {})
        .toPromise();
    } catch (e) {
      console.error(`Failed to add a vote for the ${side} side of vote with identifier ${voteId}`);
      throw e;
    }
  }
}
