async function LoadData() {
    return new Promise((resolve, reject) => {
        setTimeout(() => resolve("Data is loading..."), 2000);
    });
}

async function run() {
    await LoadData();
    console.log("Button is clicked via onclick.");
}

run();

const button = document.getElementById("btn");
button.addEventListener("click", () => {
    console.log("Button is clicked.");
});