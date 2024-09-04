import { Component, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',  
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  sidebarVisible: boolean = false;
  dropdownVisible: boolean = false;
  userName = '';
  isLogged= false;

  constructor(private auth$: AuthService) {}

  ngOnInit() 
  {        
      this.auth$.getUserName().subscribe(name => {        
        this.userName = name!;
        if(this.userName === null){
          this.isLogged= false;
        } else{
          this.isLogged= true;
        }        
      });        
  }

  logout(){
    this.auth$.signOut();
    this.dropdownVisible = false;
  }

  toggleDropdown(){
    this.dropdownVisible = !this.dropdownVisible;
  }

  toggleSidebar() {
    this.sidebarVisible = !this.sidebarVisible;
  }
}
