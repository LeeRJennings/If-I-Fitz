import { useEffect, useState } from "react";
import { getAllPosts, getCurrentUsersFavoritedPosts, searchPosts } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, Row, Input } from "reactstrap";
import { getLoggedInUser } from "../../modules/authManager";
import "./PostViews.css"

export const PostList = () => {
    const [posts, setPosts] = useState([])
    const [user, setUser] = useState({})
    const [userFavorites, setUserFavorites] = useState([])
    const [render, setRender] = useState(1)

    const navigate = useNavigate()
    
    const getUser = () => {
        getLoggedInUser()
        .then(user => setUser(user))
    }

    const getUserFavorites = () => {
        getCurrentUsersFavoritedPosts()
        .then(posts => setUserFavorites(posts))
    }
    
    const getPosts = () => {
        getAllPosts()
        .then(posts => setPosts(posts))
    }

    useEffect(() => {
        getUser()
        getPosts()
    }, [])

    useEffect(() => {
        getUserFavorites()
    }, [render])

    const handleSearch = (e) => {
        if (e.keyCode === 13) {
            searchPosts(e.target.value)
            .then(posts => {
                if (posts.length) {
                    setPosts(posts)
                } else {
                    window.alert("Sorry, no posts match that search.")
                }
            })
        }
    }

    return (
        <>
        <Input className="m-3 mb-0 mt-2" id="searchBar" type="text" placeholder="type a word or phrase then press enter to search" onKeyUp={handleSearch} />
        <Button className="m-3 mb-0 mt-2" color="success" size="lg" onClick={() => navigate("/posts/create")}>Add Post</Button>
        <Row className="m-2 mt-1">
            {posts.map((post) => (
                <Post post={post} 
                      key={post.id} 
                      user={user} 
                      userFavorites={userFavorites} 
                      render={render}
                      setRender={setRender} />
            ))}
        </Row>
        </>
    )
}