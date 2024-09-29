import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DemoService } from '../../services/demo.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add.component.html',
  styleUrl: './add.component.css'
})
export class AddComponent implements OnInit {

  demoForm!: FormGroup;


  constructor(
    private demoService: DemoService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.demoForm = this.fb.group({
      name: ['',[Validators.required, Validators.minLength(2)]],
      age: [0,[Validators.required,Validators.max(100), Validators.min(18)]]
    })
  }

  get formControl(){
    return this.demoForm.controls;
   }

   OnSubmit(){

    if(this.demoForm.valid){

      console.log(this.demoForm.value);
      this.demoService.add(this.demoForm.value).subscribe({
        next: (response) => {
          if (response.success) {   
            this.router.navigate(['/index']);       
          }
        },
        error:(err)=>{
          alert(err.error.message);
        },
        complete:()=>{
          console.log("Completed");
        }
      })
    }
  }

}
