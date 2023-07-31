import { Component, OnInit, Signal } from '@angular/core';
import { chat, message, myconversation, postconversation, users } from './models/data';
import { UsereService } from './services/usere.service';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'frontend';
  users : users[]=[];
  message!:string;
  receiverId!:number;
  senderid!:number;
  chatData:Array<chat>=[];
  myConversions:Array<myconversation>=[];
  currCovId!:number;


  private hubConnection!:signalR.HubConnection;

  constructor(private usrser:UsereService){
  
  } 

  ngOnInit(): void {
    console.log(this.senderid);

 
 
    if(this.hubConnection){
      this.hubConnection.on('receive',(mes:chat)=>{
        console.log(mes);
     
        if(mes.convId===this.currCovId){
          this.chatData.push(mes);
          console.log(mes);
        }
      });
    }
   


    this.usrser.getUsers().subscribe(res=>{
      this.users=res;
    },err=>{
      console.log(err);
    })
  }


  selectuser(event:any){
    let id=event.target.value;
    this.senderid=parseInt(id);
   
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7274/chatsocket?userId="+this.senderid).build();
    this.hubConnection.start().then(()=>{
      console.log("connection start ...");
    }).catch(err=>{
      console.log(err);
    });

    this.usrser.getMyConversation(this.senderid).subscribe(res=>{
      console.log(this.myConversions);
      this.myConversions=res;
    },err=>{
      console.log(err);
    });

  
  }
  
  sendMessage(){
      if(this.receiverId && this.senderid){
        let msg:message={
        Convid:this.currCovId,
        message:this.message,
        date:new Date(),
        writer:this.senderid as number,
        receiver:this.receiverId
      }

      this.hubConnection.invoke("SendMessage",msg).then(()=>{
          console.log(this.message);
        }).catch(err=>{
          console.log(err);
       });
      }
  }

  addtoconv(id:number){
    this.receiverId=id;
    if(this.senderid){
      let conersion:postconversation={
        userfirst:this.senderid,
        usersecond:this.receiverId
      }
      this.usrser.setConversation(conersion).subscribe(res=>{
 
        console.log(res);
      },err=>{
        console.log(err);
      })
    }else{
      alert("first select writer user")
    }
  }

  dochat(user:myconversation){
    this.receiverId=user.userid;
    this.currCovId=user.convid;

    this.usrser.getMessage(this.currCovId).subscribe(res=>{
      this.chatData=res;
    })

 

    if(this.hubConnection){
      this.hubConnection.on('receive',(mes:chat)=>{
        console.log(mes);
    
        if(mes.convId===this.currCovId){
          this.chatData.push(mes);
          console.log(mes);
        }
      });
    }
  }


  conversationexists(id:number){
    var found = this.myConversions.find(x=>x.userid==id)
    
    if(found || id==this.senderid){
      return true;
    }else{
      return false;
    }
  }
}
