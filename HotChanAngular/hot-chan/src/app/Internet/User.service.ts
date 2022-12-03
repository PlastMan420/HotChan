import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { DataService } from './Data.service';
import { eRole } from './enums';

@Injectable({
  providedIn: 'root'
})
export class UserService implements OnInit, OnDestroy {

  constructor(private dataService: DataService) { }
  
  private role: eRole = eRole.banned;
  
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit() {
    this.getUserRole();
  }

  private async getUserRole(){
    this.role = await this.dataService.getUserRole('');
  }

  get userRole(){
    return this.role;
  }
}
