import { Component, Input } from '@angular/core';
import { AverageSentiment } from '../youtube-comments-sentiment/average-sentiment';

@Component({
    selector: 'comments-sentiment-table',
    templateUrl: './comments-sentiment-table.component.html'
})
export class CommentsSentimentTableComponent {
    @Input('averageSentiment') averageSentiment: AverageSentiment;
}
