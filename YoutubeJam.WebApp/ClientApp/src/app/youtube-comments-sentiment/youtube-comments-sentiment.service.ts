import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Sentiment } from './sentiment';

@Injectable({
  providedIn: 'root'
})

export class YoutubeCommentsSentimentService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getSentiment() {
        return this.http.get<Sentiment>(this.baseUrl + 'sentiment');
    }
}
