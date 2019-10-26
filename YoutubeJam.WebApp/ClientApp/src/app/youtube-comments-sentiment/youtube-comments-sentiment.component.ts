import { Component } from '@angular/core';
import { Sentiment } from './sentiment';
import { YoutubeCommentsSentimentService } from './youtube-comments-sentiment.service'

@Component({
    selector: 'youtube-comments-sentiment',
    templateUrl: './youtube-comments-sentiment.component.html'
})
export class YoutubeCommentsSentimentComponent {
    public sentiment: Sentiment;

    constructor(private youtubeCommentsSentimentService: YoutubeCommentsSentimentService) {
    }

    getCommentsSentiment(videoId: string) {
        this.youtubeCommentsSentimentService.getSentiment(videoId).subscribe(result => {
            this.sentiment = result;
        }, error => console.error(error));
    }
}
