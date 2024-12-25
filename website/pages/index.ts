import { send } from "../utilities";

let checks = document.querySelectorAll(".favcheckbox") as NodeListOf<HTMLInputElement>;

let userId = localStorage.getItem("userId");

for (let i = 0; i < checks.length; i++) {

    checks[i].onclick = function() {
        send("favorite", [i, userId , checks[i].checked]); 
    }
}
