import { YoutubeComment } from "./youtube-comment";

export interface AverageSentiment {
    averageSentimentScore: number;
    commentList: YoutubeComment[];
}
