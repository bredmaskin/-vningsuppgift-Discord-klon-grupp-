const main = document.querySelector("main");
const inputMessage = document.getElementById("message");
const inputName = document.getElementById("name");
const submitButton = document.getElementById("submit");

async function getMessages() {
    const res = await fetch("/api/messages");
    const data = await res.json();

    main.innerHTML = "";

    data.forEach(message => {
        main.innerHTML += `<div>${message.user}: ${message.message}</div>`;
    });
}

getMessages();