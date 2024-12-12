import { send } from "../utilities";

let UsernameInput = document.querySelector("UsernameInput") as HTMLInputElement;
let PasswordInput = document.querySelector("PasswordInput") as HTMLInputElement;
let SignUpbutton = document.querySelector("SignUpButton") as HTMLButtonElement;

SignUpbutton.onclick = async function() {
let userId = await send("signup", [UsernameInput.value, PasswordInput.value]);
}

if (userId != null) {
    localStorage.setItem("userId", userId);
    top.location.href = "index.html";
}

else {
    messageDiv.innerText = "Username is already taken";
}