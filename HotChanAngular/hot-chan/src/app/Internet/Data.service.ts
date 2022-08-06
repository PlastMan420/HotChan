import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take, firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment'
import { Post } from './Types';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { 
    
  }

  public async getPostById(postId: string)
  {
    return await firstValueFrom(this.http.get(environment.devServerBaseUrl + 'post/' + postId)) as Post;
  }

}
