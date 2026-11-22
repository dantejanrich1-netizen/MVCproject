$(document).ready(function () {
    $("#btnLogin").click(function (e) {
        let username = $("#username").val().trim();
        let password = $("#password").val().trim();

        if (username === "" || password === "") {
            alert("Error: Please fill in both username and password!");
            e.preventDefault();
        } else if (username === "admin" && password === "admin123") {
            alert("Login successful! Welcome to the Instructor Information System.");
        } else {
            alert("Invalid username or password. Please try again.");
            e.preventDefault();
        }
    });

    $("#btnRegister").click(function (e) {
        let name = $("#FullName").val().trim();
        let username = $("#Username").val().trim();
        let password = $("#Password").val();
        let confirmPassword = $("#ConfirmPassword").val();

        let nameRegex = /^[a-zA-Z\s]+$/;
        let userRegex = /^[a-zA-Z0-9_]{4,15}$/;
        let passRegex = /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$/;

        if (name === "" || username === "" || password === "") {
            alert("Error: All fields are required to register!");
            e.preventDefault();
            return;
        }

        if (!nameRegex.test(name)) {
            alert("Error: Name must only contain letters and spaces.");
            e.preventDefault();
            return;
        }

        if (!userRegex.test(username)) {
            alert("Error: Username must be 4-15 characters long and contain only letters, numbers, and underscores.");
            e.preventDefault();
            return;
        }

        if (!passRegex.test(password)) {
            alert("Error: Password must be at least 8 characters long and contain at least one letter and one number.");
            e.preventDefault();
            return;
        }

        if (password !== confirmPassword) {
            alert("Error: Passwords do not match. Please try again.");
            e.preventDefault();
            return;
        }

        if (username.toLowerCase() === "admin") {
            alert("Error: This username already exists!");
            e.preventDefault();
            return;
        }

        let answer = confirm("Are you sure you want to register this account?");
        if (answer) {
            alert("Registration successful! You may now log in.");
        } else {
            alert("Registration cancelled.");
            e.preventDefault();
        }
    });
});