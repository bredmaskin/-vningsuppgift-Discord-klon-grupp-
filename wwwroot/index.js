const main = document.querySelector("main");

async function getMessages() {
    const res = await fetch("/api/messages");
    const data = await res.json();

    main.innerHTML = "";

    data.forEach(message => {
        main.innerHTML += `<div>${message.user}: ${message.message}</div>`;
    });
}

getMessages();