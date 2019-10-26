import { Component } from '@angular/core';
import { YoutubeComment } from './youtube-comment';
import { YoutubeCommentsSentimentService } from './youtube-comments-sentiment.service'

@Component({
    selector: 'youtube-comments-sentiment',
    templateUrl: './youtube-comments-sentiment.component.html'
})
export class YoutubeCommentsSentimentComponent {
    public youtubeComments: YoutubeComment[];

    constructor(private youtubeCommentsSentimentService: YoutubeCommentsSentimentService) {
    }

    getCommentsSentiment(videoText: string) {
        var url = new URL(videoText);
        this.youtubeCommentsSentimentService.getSentiment(url.searchParams.get("v")).subscribe(result => {
            this.youtubeComments = result;
        }, error => console.error(error));
    }
}
