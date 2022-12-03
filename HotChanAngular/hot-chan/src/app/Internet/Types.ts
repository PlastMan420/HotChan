export type Post = {
	postTitle: string,
	description: string,
	mediaUrl: string,
	thumbnailUrl: string,
	time: Date,
	hidden: boolean,
	userId: string
};

export type User = {
	id: string,
	userName: string,
}

export type IFile = {
	
}