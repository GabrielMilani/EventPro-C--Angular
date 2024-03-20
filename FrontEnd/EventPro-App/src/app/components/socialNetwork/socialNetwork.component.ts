import { Component, Input, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { SocialNetworkService } from '../../services/socialNetwork.service';
import { SocialNetworkModel } from '../../models/SocialNetworkModel';

@Component({
  selector: 'app-socialNetwork',
  templateUrl: './socialNetwork.component.html',
  styleUrls: ['./socialNetwork.component.scss']
})
export class SocialNetworkComponent {
  modalRef: BsModalRef;
  @Input() eventId = 0;
  public formRS: FormGroup;
  public socialNetworkCurrent = { id: 0, name: '', indice: 0 };

  public get socialNetworks(): FormArray {
    return this.formRS.get('socialNetworks') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private socialNetworkService: SocialNetworkService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
    this.loadSocialNetwork(this.eventId)
    this.validation();
  }

  private loadSocialNetwork(id: number = 0): void {
    let origin = 'speaker';

    if (this.eventId !== 0) origin = 'event';

    this.spinner.show();

    this.socialNetworkService
      .getSocialNetworks(origin, id)
      .subscribe(
        (socialNetworkRetorn: SocialNetworkModel[]) => {
          socialNetworkRetorn.forEach((socialNetwork) => {
            this.socialNetworks.push(this.createSocialNetwork(socialNetwork))
          });
        },
        (error: any) => {
          this.toastr.error('Error loaded Social Network', 'Error!');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
  }

  public validation(): void {
    this.formRS = this.fb.group({
      socialNetworks: this.fb.array([])
    })
  }

  addSocialNetwork(): void {
    this.socialNetworks.push(this.createSocialNetwork({ id: 0 } as SocialNetworkModel));
  }

  createSocialNetwork(socialNetwork: SocialNetworkModel): FormGroup {
    return this.fb.group({
      id: [socialNetwork.id],
      name: [socialNetwork.name, Validators.required],
      url: [socialNetwork.url, Validators.required]
    });
  }

  public returnTitle(name: string): string {
    return name === null || name === '' ? 'Social Network' : name;
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public saveSocialNetwork(): void {
    let origin = 'speaker';

    if (this.eventId !== 0) origin = 'event';

    if (this.formRS.controls['socialNetworks'].valid) {
      this.spinner.show();
      this.socialNetworkService
        .saveSocialNetwork(origin, this.eventId, this.formRS.value.socialNetworks)
        .subscribe(
          () => {
            this.toastr.success('Redes Sociais foram salvas com Sucesso!', 'Sucesso!');
          },
          (error: any) => {
            this.toastr.error('Erro ao tentar salvar Redes Sociais.', 'Erro');
            console.error(error);
          }
        )
        .add(() => this.spinner.hide());
    }
  }

  public removeSocialNetwork(template: TemplateRef<any>, indice: number): void {
    this.socialNetworkCurrent.id = this.socialNetworks.get(indice + '.id').value;
    this.socialNetworkCurrent.name = this.socialNetworks.get(indice + '.name').value;
    this.socialNetworkCurrent.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteSocialNetwork(): void {
    let origin = 'speaker';
    this.modalRef.hide();
    this.spinner.show();

    if (this.eventId !== 0) origin = 'event';

    this.socialNetworkService
      .deleteSocialNetwork(origin, this.eventId, this.socialNetworkCurrent.id)
      .subscribe(
        () => {
          this.toastr.success('Rede Social deletado com sucesso', 'Sucesso');
          this.socialNetworks.removeAt(this.socialNetworkCurrent.indice);
        },
        (error: any) => {
          this.toastr.error(`Erro ao tentar deletar o Rede Social `, 'Erro');
          console.error(error);
        }
      )
      .add(() => this.spinner.hide());
  }

  declineDeleteSocialNetwork(): void {
    this.modalRef.hide();
  }

}
