import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { chat, myconversation, postconversation, users } from '../models/data';

@Injectable({
  providedIn: 'root'
})
export class UsereService {

  messanger !:number
  constructor(private http:HttpClient) { }
  private readonly url ="https://localhost:7274/api";

  getUsers(){
    return this.http.get<users[]>(this.url+"/UserInfoes");
  }

  setConversation(item:postconversation){
    return this.http.post<postconversation>(this.url+"/Conversation",item);
  }

  getMyConversation(id:number){
    return this.http.get<myconversation[]>(this.url+"/Conversation/"+id);
  }

  getMessage(cid:number){
    return this.http.get<Array<chat>>(this.url+"/Message/"+cid);
  }
  
  

}
