import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor() { }
  message: string = "";
  
  add(mess: string)
  {
    this.message = mess;
  }

}
