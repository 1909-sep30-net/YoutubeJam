import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AverageSentiment } from './average-sentiment';

@Injectable({
    providedIn: 'root'
})

export class YoutubeCommentsSentimentService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getSentiment(videoId: string, maxComments: number) {
        return this.http.get<AverageSentiment>(this.baseUrl + 'sentiment', { params: new HttpParams().set("videoId", videoId).set("maxComments", maxComments.toString()) });
    }
}
