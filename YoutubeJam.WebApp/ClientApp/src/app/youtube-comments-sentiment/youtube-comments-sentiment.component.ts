import { Component, Inject } from '@angular/core';
import { Sentiment } from './sentiment';
import { YoutubeCommentsSentimentService } from './youtube-comments-sentiment.service'

@Component({
  selector: 'youtube-comments-sentiment',
  templateUrl: './youtube-comments-sentiment.component.html'
})
export class YoutubeCommentsSentimentComponent {
  public sentiment: Sentiment;

    constructor(youtubeCommentsSentimentService: YoutubeCommentsSentimentService) {
        youtubeCommentsSentimentService.getSentiment().subscribe(result => {
            this.sentiment = result;
        }, error => console.error(error));
  }
}
