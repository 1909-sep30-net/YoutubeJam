import { Component, Input } from '@angular/core';
import { YoutubeComment } from '../youtube-comments-sentiment/youtube-comment';

@Component({
    selector: 'comments-sentiment-table',
    templateUrl: './comments-sentiment-table.component.html'
})
export class CommentsSentimentTableComponent {
    @Input('youtubeComments') youtubeComments: YoutubeComment[];
}
