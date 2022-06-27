import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Team } from 'src/app/_models/team-model';
import { TeamService } from 'src/app/_services/team.service';
import { ToastrService } from 'ngx-toastr';

import * as moment from 'moment';
import { environment } from '../../../environments/environment';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-team-form',
  templateUrl: './team-form.component.html'
})
export class TeamFormComponent implements OnInit {
    formAdd: FormGroup;
  submitted = false;
  public team: Team = new Team();
  public files: any = [];

  @ViewChild('fileUpload') fileUpload: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private teamService: TeamService
  ) { }

  get q() { return this.formAdd.controls; }

  configEditor: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: '',
    translate: 'no',
    defaultParagraphSeparator: '',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      [],
      ['toggleEditorMode', 'removeFormat']
    ]
  };

  ngOnInit() {
    this.formAdd = this.formBuilder.group({
        id: [0],
        name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
        description: [''],
      });

    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.team.id = Number(params.id);
        this.load();
      }
      });
    }

    load() {
      if (this.team.id > 0) {
        this.teamService.getById(this.team.id).subscribe(result => {
          this.team = result;
          this.loadControls();
        });
      }
    }

    loadControls() {
      this.formAdd.controls.id.setValue(this.team.id);
      this.formAdd.controls.name.setValue(this.team.name);
      this.formAdd.controls.description.setValue(this.team.description);
    }

    onSave() {
      this.submitted = true;
      if (this.formAdd.invalid) {
        return;
      }
      const formData = new FormData();
      this.team.name = this.formAdd.controls.name.value;
      this.team.description = this.formAdd.controls.description.value;
      formData.append('team', JSON.stringify(this.team));
      if(this.files.length > 0) {
        this.files.forEach(f => {
          formData.append('file', f.file, f.file.name);
      });

      }
      this.teamService.save(formData).subscribe(result => {
        this.toastr.success('Registro efetuado com sucesso!');
        this.router.navigate(['partner-area/team']);
    });
    }

    onCancel() {
      this.router.navigate([`partner-area/team`]);
    }
    onFileChange(event) {

      const extension = event.target.files[0].name.split('.');
      // if (extension[1] !== 'jpg') {
      //   this.onResetFileChange();
      //   this.toastr.error('Só é permitido arquivos no formato JPG');
      //   return;
      // }
      if (event.target.files.length > 3) {
      return this.toastr.error('Só é permitido anexar três arquivos ou menos!');
      }

      if (event.target.files.length > 0) {
        for (const file of event.target.files) {
          this.files.push({ file });
        }
      }


    }

    onResetFileChange() {
      this.fileUpload.nativeElement.value = '';
  }

  getImage(nomeImage) {
    return environment.urlImagesTeam + nomeImage;
}

deleteRow(teamImage) {
  const index: number = this.team.teamImages.indexOf(teamImage);
  this.team.teamImages.splice(index, 1);
}


}


