// Fungsi yang dipanggil saat tombol diklik
function TSButton() {
    let user = new Student("Hafizs", "Fauzi", "Ritonga");
    document.getElementById("ts-example")!.innerHTML = greeter(user);
}

// Kelas Student dengan properti fullName dan constructor untuk inisialisasi
class Student {
    fullName: string;

    // Constructor menerima firstName, middleInitial, dan lastName sebagai parameter
    constructor(public firstName: string, public middleInitial: string, public lastName: string) {
        // Menggabungkan firstName, middleInitial, dan lastName untuk membuat fullName
        this.fullName = firstName + " " + middleInitial + " " + lastName;
    }
}

// Interface Person dengan properti firstName dan lastName
interface Person {
    firstName: string;
    lastName: string;
}

// Fungsi greeter yang menerima parameter bertipe Person dan mengembalikan string salam
function greeter(person: Person) {
    // Mengembalikan string salam yang memuat nama depan dan nama belakang
    return "Hello, " + person.firstName + " " + person.lastName;
}

// Membuat instance Student dengan nama Fred
let user = new Student("Hafizs", "Fauzi", "Ritonga");
