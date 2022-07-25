import { useEffect, useState } from "react";
import { getAllPosts } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, Row } from "reactstrap";

export const PostList = ({ user }) => {
    const [posts, setPosts] = useState([])
    
    const navigate = useNavigate()

    const getPosts = () => {
        getAllPosts()
        .then(posts => setPosts(posts))
    }

    useEffect(() => {
        getPosts()
    }, [])

    return (
        <>
        <Button color="success" size="lg" onClick={() =>navigate("/posts/create")}>Add Post</Button>
        <Row>
            {posts.map((post) => (
                <Post post={post} key={post.id} user={user} />
            ))}
        </Row>
        </>
    )
}