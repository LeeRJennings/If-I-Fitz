import { Button, Card, CardBody, CardTitle, CardSubtitle, CardText } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import firebase from "firebase/app";
import "firebase/auth";

export const Post = ({ post }) => {
    const navigate = useNavigate()

    return (
        <Card color="light" style={{width: '20rem'}}>
            <CardBody>
                <CardTitle tag="h5">
                    {post.title}
                </CardTitle>
                <CardSubtitle className="mb-2 text-muted" tag="h6">
                    Posted by: {post.userProfile.name} on {post.createdDateTime}
                </CardSubtitle>
            </CardBody>
                <img alt="probably a cat in a box" src={post.imageLocation} width="100%"/>
            <CardBody>
                <CardText className="mb-2 text-muted">
                    <b>Size:</b> {post.size.name} | <b>Material:</b> {post.material.type}
                </CardText>
                <CardText>
                    {post.description}
                </CardText>
                <Button color="primary" onClick={() => navigate(`/posts/edit/${post.id}`)}>Edit</Button>
                <Button color="danger" onClick={() => navigate(`/posts/delete/${post.id}`)}>Delete</Button>
            </CardBody>
        </Card>
    )
}