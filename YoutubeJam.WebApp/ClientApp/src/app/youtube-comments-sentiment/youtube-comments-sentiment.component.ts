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

    getCommentsSentiment(videoText: string) {
        var url = new URL(videoText);
        this.youtubeCommentsSentimentService.getSentiment(url.searchParams.get("v")).subscribe(result => {
            this.sentiment = result;
        }, error => console.error(error));
    }
}
