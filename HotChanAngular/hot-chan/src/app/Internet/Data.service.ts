import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take, firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment';
import { eRole } from './enums';
//import { Post, UserAuth } from './Types';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { 
    
  }

  // public async getPostById(postId: string)
  // {
  //   return await firstValueFrom(this.http.get<Post>(environment.devServerBaseUrl + 'post/GetPost/' + postId));
  // }

  // public async getUserRole(userId: string)
  // {
  //   return await firstValueFrom(this.http.get<eRole>(environment.devServerBaseUrl + 'user/GetUserRole/' + userId));
  // }

  // public async CreateUser(data: UserAuth)
  // {
  //   return await firstValueFrom(this.http.post(environment.devServerBaseUrl + 'user/Register/', data));
  // }

}
