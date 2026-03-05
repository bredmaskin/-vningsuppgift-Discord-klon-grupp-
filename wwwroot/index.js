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

async function postMessage(user, message) {
    await fetch("/api/messages", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ user, message })
    });
    getMessages();
}

submitButton.addEventListener('click', function (event) {
    event.preventDefault();
    const user = inputName.value.trim();
    const message = inputMessage.value.trim();

    postMessage(user, message);
    inputMessage.value = "";
}),

getMessages();