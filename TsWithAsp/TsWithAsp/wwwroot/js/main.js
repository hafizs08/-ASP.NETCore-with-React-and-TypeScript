"use strict";
// Fungsi yang dipanggil saat tombol diklik
function TSButton() {
    let user = new Student("Hafizs", "Fauzi", "Ritonga");
    document.getElementById("ts-example").innerHTML = greeter(user);
}
// Kelas Student dengan properti fullName dan constructor untuk inisialisasi
class Student {
    // Constructor menerima firstName, middleInitial, dan lastName sebagai parameter
    constructor(firstName, middleInitial, lastName) {
        this.firstName = firstName;
        this.middleInitial = middleInitial;
        this.lastName = lastName;
        // Menggabungkan firstName, middleInitial, dan lastName untuk membuat fullName
        this.fullName = firstName + " " + middleInitial + " " + lastName;
    }
}
// Fungsi greeter yang menerima parameter bertipe Person dan mengembalikan string salam
function greeter(person) {
    // Mengembalikan string salam yang memuat nama depan dan nama belakang
    return "Hello, " + person.firstName + " " + person.lastName;
}
// Membuat instance Student dengan nama Fred
let user = new Student("Hafizs", "Fauzi", "Ritonga");
