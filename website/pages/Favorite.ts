import { send } from "../utilities";

let dances = document.querySelectorAll(".dance") as NodeListOf<HTMLDivElement>;

let userId = localStorage.getItem("userId");

let Favorite = await send(
    "getfavorite"
    , userId

) as boolean[]; 

console.log(Favorite);

for (let i = 0; i < Favorite.length; i++) {
    if (Favorite[i]) {
        dances[i].style.display = "block";
    }
}