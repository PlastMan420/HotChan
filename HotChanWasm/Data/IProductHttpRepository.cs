﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HotChanWasm.Data
{
	public interface IProductHttpRepository
	{
		//Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
		//Task CreateProduct(Product product);
		Task<string> UploadProductImage(MultipartFormDataContent content);
	}
}
