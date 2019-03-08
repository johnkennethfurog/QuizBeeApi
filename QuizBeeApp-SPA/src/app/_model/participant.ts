export interface Participant {
    id:number;
    name:string;
    isVerify:boolean;
    eventCode?:string;
    referenceNumber:string;
    totalScores?:number;
}
