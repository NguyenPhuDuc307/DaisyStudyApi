package com.example.daisystudyapp;

import com.google.gson.annotations.SerializedName;

import java.time.LocalDateTime;
import java.util.Calendar;
import java.util.UUID;

public class User {
    @SerializedName("UserId")
    private UUID UserId;
    @SerializedName("Email")
    private String Email;
    @SerializedName("Password")
    private String Password;
    @SerializedName("FullName")
    private String FullName;
    @SerializedName("Dob")
    private Calendar Dob;
    @SerializedName("Avatar")
    private String Avatar;

    public User() {
    }

    public User(UUID userId, String email, String password, String fullName, Calendar dob, String avatar) {
        UserId = userId;
        Email = email;
        Password = password;
        FullName = fullName;
        Dob = dob;
        Avatar = avatar;
    }

    public UUID getUserId() {
        return UserId;
    }

    public void setUserId(UUID userId) {
        UserId = userId;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getFullName() {
        return FullName;
    }

    public void setFullName(String fullName) {
        FullName = fullName;
    }

    public Calendar getDob() {
        return Dob;
    }

    public void setDob(Calendar dob) {
        Dob = dob;
    }

    public String getAvatar() {
        return Avatar;
    }

    public void setAvatar(String avatar) {
        Avatar = avatar;
    }
}
