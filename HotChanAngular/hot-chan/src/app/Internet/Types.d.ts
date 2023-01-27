export type Post = {
	postTitle: string,
	description: string,
	mediaUrl: string,
	thumbnailUrl: string,
	createdOn: Date | string,
	hidden: boolean,
	postId: string,
	tags: string[]
};

export type User = {
	id: string,
	userName: string,
};

export type IFile = {
	
};

export type UserAuth = {
	username: string,
	password: string,
	key: string
};
