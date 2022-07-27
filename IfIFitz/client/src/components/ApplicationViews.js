import React, { useEffect, useState } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { getFavoritedPostsByUserId } from "../modules/postManager";
import Login from "./Login";
import { FavoritePosts } from "./Posts/FavoritePosts";
import { PostByUser } from "./Posts/PostByUser";
import { PostDelete } from "./Posts/PostDelete";
import { PostDetails } from "./Posts/PostDetails";
import { PostEdit } from "./Posts/PostEdit";
import { PostForm } from "./Posts/PostForm";
import { PostList } from "./Posts/PostList";
import Register from "./Register";
import { Welcome } from "./Welcome";

export default function ApplicationViews({ isLoggedIn, user }) {
  const [userFavorites, setUserFavorites] = useState([])
  const [render, setRender] = useState(1)

  const getUserFavorites = () => {
    getFavoritedPostsByUserId(user.id)
    .then(posts => setUserFavorites(posts))
  }

  useEffect(() => {
    getUserFavorites()
  },[render])

  return (
    <Routes>
      <Route path="/">
        <Route index element={isLoggedIn ? <Welcome /> : <Navigate to="/login" />} />
        
        <Route path="posts">
          <Route index element={isLoggedIn ? <PostList user={user} 
                                                       userFavorites={userFavorites} 
                                                       render={render}
                                                       setRender={setRender} 
                                                       /> : <Navigate to="/login" />} />

          <Route path="create" element={isLoggedIn ? <PostForm /> : <Navigate to="/login" />} />
          <Route path="edit/:id" element={isLoggedIn ? <PostEdit /> : <Navigate to="/login" />} />
          <Route path="delete/:id" element={isLoggedIn ? <PostDelete /> : <Navigate to="/login" />} />
          <Route path="myPosts" element={isLoggedIn ? <PostByUser user={user} 
                                                                  userFavorites={userFavorites} 
                                                                  render={render} 
                                                                  setRender={setRender} 
                                                                  /> : <Navigate to="/login" />} />
                                                                  
          <Route path="favoritePosts" element={isLoggedIn ? <FavoritePosts user={user} 
                                                                           userFavorites={userFavorites} 
                                                                           render={render} 
                                                                           setRender={setRender} 
                                                                           /> : <Navigate to="/login" />} />

          <Route path=":id" element={isLoggedIn ? <PostDetails user={user} 
                                                               userFavorites={userFavorites} 
                                                               render={render} 
                                                               setRender={setRender} 
                                                               /> : <Navigate to="/login" />} />
        </Route>
        
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}