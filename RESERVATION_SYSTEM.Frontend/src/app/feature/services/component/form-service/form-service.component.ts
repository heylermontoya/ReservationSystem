import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RegularExpressions } from '../../../../shared/constant/regex';
import { CreateOrUpdateService } from '../../../../shared/interfaces/createOrUpdateService.interface';
import { ServicesService } from '../../../../shared/services/Service/services.service';

@Component({
  selector: 'app-form-service',  
  templateUrl: './form-service.component.html',
  styleUrl: './form-service.component.scss'
})
export class FormServiceComponent {
  serviceForm: FormGroup;
  requiredField = 'Required field';
  serviceName = '';
  action = '';
  id= '';
  possitiveNumber = 'Positive number greater than or equal to zero';
  possitiveNumberInt = 'Positive number integer greater than or equal to zero';

  constructor(    
    private formBuilder: FormBuilder, 
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private serviceService: ServicesService,
  ) {
    this.serviceForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price:[null,[Validators.required, Validators.min(0),Validators.pattern(RegularExpressions.NUMERIC_PART)]],
      capacity: [null,[Validators.required, Validators.min(0),Validators.pattern(RegularExpressions.NUMERIC)]]
    });
  }

  ngOnInit() {
    this.action = this.config.data.action;

    this.serviceName = this.config.data.name;
    if (this.action === 'update'){
      this.id = this.config.data.id;
      this.serviceForm.setValue({
        name: this.serviceName,
        description: this.config.data.description,
        price: this.config.data.price,
        capacity: this.config.data.capacity
      });
    }
  }

  onSubmit() {
    if (this.serviceForm.valid) {
      if(this.action === 'create') {
        const serviceParser = this.serviceCreateParserFormData();
        this.serviceService.createService(serviceParser).subscribe({
          next: () => {
                this.onClose();   
          }, error: (error) => {
            if(error?.status === 400){
              alert(error.error.message);
            }
          }
        });
      } else{
        const serviceParser = this.serviceUpdateParserFormData();
        this.serviceService.updateService(serviceParser).subscribe({
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
    this.serviceForm.reset();
    this.ref.close();
  }

  private serviceCreateParserFormData() : CreateOrUpdateService{
    return {
      name: this.serviceForm.value.name,
      description: this.serviceForm.value.description,
      price: this.serviceForm.value.price,
      capacity: this.serviceForm.value.capacity,
    }
  }
  
  private serviceUpdateParserFormData(): CreateOrUpdateService{
    return {
      id: this.id,
      name: this.serviceForm.value.name,
      description: this.serviceForm.value.description,
      price: this.serviceForm.value.price,
      capacity: this.serviceForm.value.capacity,
    }
  }
}
