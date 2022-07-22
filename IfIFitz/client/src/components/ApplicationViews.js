import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import { PostList } from "./Posts/PostList";
import Register from "./Register";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route index element={isLoggedIn ? <PostList /> : <Navigate to="/login" />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}