import { Data } from "@angular/router";

export interface users{
    userId:number,
    userName: string,
    password:string
}


export interface message{
    Convid:number
    message:string;
    date:Date;
    writer:number;
    receiver:number;
}

export interface postconversation{
    userfirst:number,
    usersecond:number
}

export interface myconversation{
    convid:number
    userid:number,
    username: string
}

export interface chat{
    message :string,
    author:number,
    convId:number,
    name:string,
}