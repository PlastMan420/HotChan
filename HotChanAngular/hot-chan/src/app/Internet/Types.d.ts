export type Post = {
	postTitle: string,
	description: string,
	mediaUrl: string,
	thumbnailUrl: string,
	createdOn: Date | string,
	hidden: boolean,
	postId: string,
	tags: string[],
	score: number
};

export type User = {
	id: string,
	userName: string,
};

export type IFile = {
	
};

export type UserRegisterFormDtoInput = {
	userName: string,
	userMail: string,
	key: string
};

export type UserLoginDtoInput = {
	username: string,
	key: string
};
