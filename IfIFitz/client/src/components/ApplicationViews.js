import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./Login";
import { PostDelete } from "./Posts/PostDelete";
import { PostEdit } from "./Posts/PostEdit";
import { PostForm } from "./Posts/PostForm";
import { PostList } from "./Posts/PostList";
import Register from "./Register";
import { Welcome } from "./Welcome";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route index element={isLoggedIn ? <Welcome /> : <Navigate to="/login" />} />
        
        <Route path="posts">
          <Route index element={isLoggedIn ? <PostList /> : <Navigate to="/login" />} />
          <Route path="create" element={isLoggedIn ? <PostForm /> : <Navigate to="/login" />} />
          <Route path="edit/:id" element={isLoggedIn ? <PostEdit /> : <Navigate to="/login" />} />
          <Route path="delete/:id" element={isLoggedIn ? <PostDelete /> : <Navigate to="/login" />} />
        </Route>
        
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}