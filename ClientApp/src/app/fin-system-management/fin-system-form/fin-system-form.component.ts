import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FinSystem } from 'src/app/_models/fin-system-model';
import { FinSystemService } from 'src/app/_services/fin-system.service';
import { ToastrService } from 'ngx-toastr';
import { FinSystemColor } from 'src/app/_models/fin-system-color-model';

@Component({
  selector: 'app-fin-system-form',
  templateUrl: './fin-system-form.component.html'
})
export class FinSystemFormComponent implements OnInit {
  formAdd: FormGroup;
  img: any;
  submitted = false;
  public finSystem: FinSystem = new FinSystem();
  public files: any = [];
  hasSelected: any;
  colors: any;
  @ViewChild('selectAll') private selectAll: any;


  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private finSystemService: FinSystemService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
        this.finSystem.id = Number(params.id);
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
      details: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(500)]],
    });

    this.load();



  }

  load() {

    if (this.finSystem.id > 0) {
      this.finSystemService.getById(this.finSystem.id).subscribe(result => {
        this.finSystem = result;
        this.formAdd.controls.id.setValue(this.finSystem.id);
        this.formAdd.controls.name.setValue(this.finSystem.name);
        this.formAdd.controls.details.setValue(this.finSystem.details);
      });
    }


  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }

    const formData = new FormData();
    const item = new FinSystem();
    item.id = this.finSystem.id;
    item.details = this.formAdd.controls.details.value;
    item.name = this.formAdd.controls.name.value;

    formData.append('finSystem', JSON.stringify(item));
    if(this.files.length > 0) {
      this.files.forEach(f => {
        formData.append('file', f.file, f.file.name);
    });
    }

    this.finSystemService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/fin-system']);
    });
  }

  onCancel() {
    this.router.navigate([`partner-area/fin-system`]);
  }



  toggleAll() {
    const allEqual: boolean = this.selectAll.nativeElement.checked;
    this.colors.forEach(row => {
      row.isSelected = allEqual;
    });
    this.hasSelected = allEqual;
  }

  toggle(rowData: FinSystemColor) {
    let allEqual = true;
    this.hasSelected = false;
    this.colors.forEach(row => {
      if (row.isSelected) {
        this.hasSelected = true;
      }
      if (rowData.isSelected !== row.isSelected) {
        allEqual = false;
        return false;
      }
    });
    if (allEqual) {
      allEqual = rowData.isSelected;
    }
    this.selectAll.nativeElement.checked = allEqual;
  }

  onFileChange(event) {
    if (event.target.files.length > 4) {
    return this.toastr.error('Só é permitido anexar um arquivo!');
    }

    if (event.target.files.length > 0) {
      this.onResetFileChange();
      this.files = [];
      for (const file of event.target.files) {
        this.files.push({ file });
      }
    }
  }

  onResetFileChange() {
    this.files = [];
}








}

