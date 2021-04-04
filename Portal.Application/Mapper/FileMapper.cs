using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.Application.Mapper
{
    public class FileMapper
    {
        public static Files MapMulit(FileAndTagUploadDto multiFileUploadDto, int userId)
        {
            var newFile = new Files();

            var fileTags = new FileTag();

            newFile.FileGroupId = multiFileUploadDto.FileGroupId;
            newFile.FileName = multiFileUploadDto.FileName;
            newFile.PrimeryKey = multiFileUploadDto.PrimeryKey;
            newFile.TableName = multiFileUploadDto.TableName;
            newFile.RefferKey = multiFileUploadDto.RefferKey;
            newFile.RefferKey = multiFileUploadDto.RefferKey;
            newFile.CreateDate = multiFileUploadDto.CreateDate.TryToDateTime();


            foreach (var item in multiFileUploadDto.Tags)
            {
                newFile.FileTag.Add(new FileTag
                {
                    UserId = userId,
                    TagName = item,
                    CreateDate=DateTime.Now,
                    
                });
            }

            return newFile;
        }
    }
}
