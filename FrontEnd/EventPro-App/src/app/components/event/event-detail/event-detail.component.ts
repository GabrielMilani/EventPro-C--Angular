import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrl: './event-detail.component.scss'
})
export class EventDetailComponent {

form!: FormGroup;

public get f(): any
{
  return this.form.controls;
}

constructor(private fb: FormBuilder){}

ngOnInit(): void
{
  this.validation();
}

public validation(): void
{
  this.form = this.fb.group({
    theme: ['',[Validators.required, Validators.minLength(4), Validators.maxLength(100)]],
    local: ['', Validators.required],
    eventDate: ['', Validators.required],
    quantityPeople: ['', [Validators.required, Validators.max(120000)]],
    email: ['', [Validators.required, Validators.email]],
    imageUrl: ['', Validators.required],
  });
}

public resetForm(): void
{
  this.form.reset();
}

}
