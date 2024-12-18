import { send } from "../utilities"

let UsernameInput = document.querySelector("#UsernameInput")! as HTMLInputElement;
let PasswordInput = document.querySelector("#PasswordInput")! as HTMLInputElement;
let Loginbutton = document.querySelector("#LoginButton")! as HTMLButtonElement;

Loginbutton.onclick = async function () {
    let [userFound, userId] = await send("login", [UsernameInput.value, PasswordInput.value]) as [boolean, string];
    console.log("user found: " + userFound)
  
    if (userFound) {
    localStorage.setItem("userId", userId);
    location.href="\website\pages\index.html";
    }
}