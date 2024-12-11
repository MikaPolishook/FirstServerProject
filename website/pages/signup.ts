import { send } from "../utilities";

let UsernameInput = document.querySelector("UsernameInput") as HTMLInputElement;
let PasswordInput = document.querySelector("PasswordInput") as HTMLInputElement;
let SignUpbutton = document.querySelector("SignUpButton") as HTMLButtonElement;

SignUpbutton.onclick = function() {
send("signup", [UsernameInput.value, PasswordInput.value]);
}
