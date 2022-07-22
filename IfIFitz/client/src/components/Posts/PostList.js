import { useEffect, useState } from "react";
import { getAllPosts } from "../../modules/postManager";
import { Post } from "./Post";
import { useNavigate } from "react-router-dom";
import { Button, CardGroup, Row } from "reactstrap";

export const PostList = () => {
    const [posts, setPosts] = useState([]);
    const navigate = useNavigate();

    const getPosts = () => {
        getAllPosts()
        .then(posts => setPosts(posts))
    }

    useEffect(() => {
        getPosts()
    }, [])

    return (
        <>
        {/* <Button className="btn btn-success" onClick={() =>navigate("/posts/create")}>Add Post</Button> */}
        <Row>
        {posts.map((post) => (
            <Post post={post} key={post.id} />
        ))}
        </Row>
        </>
    )
}