export interface CreateOrUpdateReservation{
    id?:string;
    customerId?: string;
    ServiceId: string;
    dateReservation: string | Date;
    startDate: string | Date;
    endDate: string | Date;
    numberPeople: string;
    total: string;
}