import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EventModel} from '../../models/EventModel';
import { EventService } from '../../services/event.service';
import { Component, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-event',
  templateUrl: './event.component.html',
  styleUrl: './event.component.scss'
})
export class EventComponent {

ngOnInit(): void {}

}
