import { Component } from '@angular/core';
import { FieldFilter } from '../../shared/interfaces/FieldFilter.interface';
import { HistoryReservations } from '../../shared/interfaces/historyReservations.interface';
import { HistoryReservationService } from '../../shared/services/historyReservation/history-reservation.service';

@Component({
  selector: 'app-history-reservation',
  templateUrl: './history-reservation.component.html',
  styleUrl: './history-reservation.component.scss'
})
export class HistoryReservationComponent {
  historyReservations!: HistoryReservations[];
  searchValue = '';
  loading: boolean = true;
  filters: FieldFilter[] = [];

  constructor(
    private historyReservationService: HistoryReservationService, 
  ) {}

  ngOnInit() {
    this.getHistoryReservation();
  } 

  getHistoryReservation(searchTerm: string = ''){
    this.historyReservationService.getHistoryReservation(this.filters).subscribe({
      next: response => {
        this.historyReservations = response;
        this.loading = false;

        this.historyReservations.forEach((historyReservation) => {
          historyReservation.dateChange = new Date(<Date>historyReservation.dateChange);
        });
      }
    })
  }    

  onColumnFilter(event: any, field: string) {
    const value = event.target.value.trim();

    const data = {
      field: field,
      value : value
    }

    //
    const indiceExist = this.filters.findIndex(item => item.field === data.field);

    if(indiceExist !== -1) {
      this.filters.splice(indiceExist, 1);
    }

    if(data.value){
      this.filters.push(data);
    }

    //
    this.getHistoryReservation(); 
  }

  onDateFilter(event: any, field: string) {
    const value = event ? new Date(event).toISOString().split('T')[0] : '';

    const data = {
        field: field,
        value: value,
        typeDateTime: 3
    };

    const indiceExist = this.filters.findIndex(item => item.field === data.field);

    if (indiceExist !== -1) {
        this.filters.splice(indiceExist, 1);
    }

    if (data.value) {
        this.filters.push(data);
    }

    this.getHistoryReservation(); 
  }
}
