import { Component } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { AverageSentiment } from './average-sentiment';
import { YoutubeCommentsSentimentService } from './youtube-comments-sentiment.service';

@Component({
    selector: 'youtube-comments-sentiment',
    templateUrl: './youtube-comments-sentiment.component.html',
    styleUrls: ['./youtube-comments-sentiment.component.css']
})
export class YoutubeCommentsSentimentComponent {
    safeEmbedUrl: SafeResourceUrl;
    averageSentiment: AverageSentiment;

    constructor(
        private youtubeCommentsSentimentService: YoutubeCommentsSentimentService,
        private sanitizer: DomSanitizer) {
    }

    getCommentsSentiment(videoText: string) {
        var url = new URL(videoText);
        var videoId = url.searchParams.get("v");
        this.safeEmbedUrl = this.sanitizer.bypassSecurityTrustResourceUrl("https://www.youtube.com/embed/" + videoId);
        this.youtubeCommentsSentimentService.getSentiment(videoId).subscribe(result => {
            this.averageSentiment = result;
            console.log(this.averageSentiment);
        }, error => console.error(error));
    }
}
