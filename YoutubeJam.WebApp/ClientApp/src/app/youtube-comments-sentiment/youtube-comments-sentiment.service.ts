import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { YoutubeComment } from './youtube-comment';

@Injectable({
    providedIn: 'root'
})

export class YoutubeCommentsSentimentService {
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getSentiment(videoId: string) {
        return this.http.get<YoutubeComment[]>(this.baseUrl + 'sentiment', { params: new HttpParams().set("videoId", videoId) });
    }
}
