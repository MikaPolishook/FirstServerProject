import { send } from "../utilities";

let UsernameInput = document.querySelector("#UsernameInput")! as HTMLInputElement;
let PasswordInput = document.querySelector("#PasswordInput")! as HTMLInputElement;
let SignUpButton = document.querySelector("#SignUpButton")! as HTMLButtonElement;

SignUpButton.onclick = async function () {
    let userId = await send("signup" , [UsernameInput.value, PasswordInput.value]) as string;
    localStorage.setItem("userId", userId);
  }
