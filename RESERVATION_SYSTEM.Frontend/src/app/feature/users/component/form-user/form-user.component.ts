import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CustomerService } from '../../../../shared/services/customer/customer.service';

@Component({
  selector: 'app-form-user',  
  templateUrl: './form-user.component.html',
  styleUrl: './form-user.component.scss'
})
export class FormUserComponent {

  userForm: FormGroup;
  requiredField = 'Required field';
  customerName = '';
  action = '';
  id= '';

  constructor(    
    private formBuilder: FormBuilder, 
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private customerService: CustomerService
  ) {
    this.userForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.action = this.config.data.action;

    this.customerName = this.config.data.name;

    if (this.action === 'update'){
      this.id = this.config.data.id;
      this.userForm.setValue({
        name: this.customerName,
        email: this.config.data.email,
        phone: this.config.data.phone
      });
    }
  }

  onSubmit() {
    if (this.userForm.valid) {
      if(this.action === 'create') {
        const userParser = this.userCreateParserFormData();
        this.customerService.createUser(userParser).subscribe({
          next: () => {
                this.onClose();   
          }, error: (error) => {
            if(error?.status === 400){
              alert(error.error.message);
            }
          }
        });
      } else{
        const userParser = this.userUpdateParserFormData();
        this.customerService.updateUser(userParser).subscribe({
          next: () => {
                this.onClose();   
          }, error: (error) => {
            if(error?.status === 400){
              alert(error.error.message);
            }
          }
        });
      }
    }
  }

  onClose() {
    this.userForm.reset();
    this.ref.close();
  }

  private userCreateParserFormData(){
    return {
      name: this.userForm.value.name,
      email: this.userForm.value.email,
      phone: this.userForm.value.phone
    }
  }
  
  private userUpdateParserFormData(){
    return {
      id: this.id,
      name: this.userForm.value.name,
      email: this.userForm.value.email,
      phone: this.userForm.value.phone
    }
  }
}
